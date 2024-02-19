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
        if (Number.isNaN(idOrUserName))
            return await this.db.users.get({ userName: idOrUserName });
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
        const userGames = await this.getUserGames(userId);
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

    async getUserGames(userId) {
        return await this.db.games.where('userId').equals(userId).toArray();
    }

    async getUserGamesOfLevel(userId, level) {
        return await this.db.games.where('userId').equals(userId).and(game => game.level === level).sortBy('level');
    }

    async getBestGame(level) {
        const games = await this.db.games.where('level').equals(level).reverse().sortBy('score');
        console.log(games);
        return games.length > 0 ? games[0] : None;
    }

    async getUserStats(userId, level = 0) {
        let userGames = [];

        if (level === 0)
            userGames = await this.getUserGames(userId);
        else
            userGames = await this.getUserGamesOfLevel(userId, level);

        // Create an empty object to store stats per level
        const levelStats = {};

        // Iterate through each game and update stats per level
        for (const game of userGames) {
            const level = game.level;
            // Initialize stats for the level if it doesn't exist
            if (!levelStats[level]) {
                levelStats[level] = {
                    highScore: 0,
                    timesPlayed: 0,
                    timesWon: 0,
                    timesLost: 0,
                };
            }

            // Update level stats for this game
            levelStats[level].highScore = Math.max(levelStats[level].highScore, game.score);
            levelStats[level].timesPlayed++;
            levelStats[level].timesWon += game.gameWon ? 1 : 0;
            levelStats[level].timesLost += game.gameLost ? 1 : 0;
        }



        // Convert level stats object to an array of LevelDTO objects
        const levelData = Object.entries(levelStats).map(([level, stats]) => {
            return new LevelStatsDTO({
                userId: userId,
                level: parseInt(level), // Ensure level is number
                highScore: stats.highScore,
                timesPlayed: stats.timesPlayed,
                timesWon: stats.timesWon,
                timesLost: stats.timesLost,
                usersBestGame: userGames.find(game => game.level === parseInt(level) && game.score === stats.highScore),
            });
        });


        for (let i = 0; i < levelData.length; i++) {
            levelData[i].levelsBestGame = await this.getBestGame(levelData[i].level)
        }

        return levelData;
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

export class LevelStatsDTO {
    constructor(levelData) {
        this.userId = levelData.userId;
        this.level = levelData.level;
        this.highScore = levelData.highScore;
        this.timesPlayed = levelData.timesPlayed;
        this.timesWon = levelData.timesWon;
        this.timesLost = levelData.timesLost;
        this.usersBestGame = levelData.usersBestGame;
        this.levelsBestGame = levelData.levelsBestGame;
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