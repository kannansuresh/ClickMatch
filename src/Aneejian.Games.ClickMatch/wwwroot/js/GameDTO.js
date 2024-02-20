
export class GameDTO {
    constructor(game) {
        this.id = game.id === 0 || null ? undefined : game.id;
        this.userId = game.userId;
        this.level = game.level;
        this.score = game.score;
        this.timeTaken = game.timeTaken;
        this.gameWon = game.gameWon;
        this.gameLost = game.gameLost;
        this.gameAbandoned = game.gameAbandoned;
    }
}
