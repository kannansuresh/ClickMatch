export class IndexedDb {
    constructor() {
        this.db = new Dexie('Aneejian.Games.ClickMatch');
        this.setupDatabase();
    }

    setupDatabase() {
        this.db.version(1).stores({
            users: "++id, &userName",
            games: "++id, userId, level, score, timeTaken, gameWon, gameAbandoned"
        });
        this.db.users.mapToClass(UserDTO);
    }

    async addNewUser(user) {
        return await this.db.users.add(new UserDTO(user));
    }

    async addUserGame(game) {
        return await this.db.games.add(game);
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
        if (isNaN(idOrUserName))
            return await this.db.users.delete({ userName: idOrUserName });
        else
            return await this.db.users.delete(idOrUserName);
    }

    // Other database operations can be added here, such as update, delete, etc.
}

export class UserDTO {

    constructor(user) {
        this.userName = user.userName;
        this.name = user.name;
        this.password = user.password;
        this.avatar = user.avatar;
    }
}

export function createIndexedDb() {
    return new IndexedDb();
}