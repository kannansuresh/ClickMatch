export class IndexedDb {
    constructor() {
        this.db = new Dexie('Aneejian.Games.ClickMatch');
        this.setupDatabase();
    }

    setupDatabase() {
        this.db.version(1).stores({
            users: "++id, &userName",
            games: "++id, userId, level, score, timeTaken, gameWon, gameLost, gameAbandoned"
        });
        this.db.users.mapToClass(UserDTO);
        this.db.games.mapToClass(GameDTO);
    }

    async addNewUser(user) {
        return await this.db.users.add(new UserDTO(user));
    }

    async addUserGame(game) {
        return await this.db.games.add(new GameDTO(game));
    }

    async updateUserGame(game) {
        return await this.db.games.update(game.id, new GameDTO(game));
    }

    async getUsers() {
        return await this.db.users.toArray();
    }

    async getUser(idOrUserName) {
        if (isNaN(idOrUserName))
            return await this.db.users.get({ userName: idOrUserName });
        else
            return await this.db.users.get(idOrUserName);
    }

    async userExists(userName) {
        return !! await this.getUser(userName);
    }

    async deleteUser(idOrUserName) {
        const user = await this.getUser(idOrUserName);
        await this.db.games.where('userId').equals(user.id).delete();
        return await this.db.users.delete(user.id)
    }

    async getUserMaxGameLevel(userId) {
        const userGames = await this.getGames(userId);
        if (userGames.length === 0)
            return 0;
        const levelsWon = new Set(userGames.filter(game => game.gameWon).map(game => game.level));
        if (levelsWon.size === 0)
            return 0;
        const maxLevel = Math.max(...levelsWon);
        if (maxLevel === levelsWon.size)
            return maxLevel;
        const missingLevel = levelsWon.size > 0 ? levelsWon.find((level) => !levelsWon.has(level + 1)) : 0;
        return missingLevel - 1 || Math.max(...levelsWon);
    }

    async getGames(userId) {
        return await this.db.games.where('userId').equals(userId).toArray();
    }

    // Other database operations can be added here, such as update, delete, etc.
}

export class UserDTO {
    constructor(user) {
        this.id = user.id == 0 || null ? undefined : user.id;
        this.userName = user.userName;
        this.name = user.name;
        this.password = user.password;
        this.avatar = user.avatar;
    }
}

export class GameDTO {
    constructor(game) {
        this.id = game.id == 0 || null ? undefined : game.id;
        this.userId = game.userId;
        this.level = game.level;
        this.score = game.score;
        this.timeTaken = game.timeTaken;
        this.gameWon = game.gameWon;
        this.gameLost = game.gameLost;
        this.gameAbandoned = game.gameAbandoned;
    }
}

export function createIndexedDb() {
    return new IndexedDb();
}