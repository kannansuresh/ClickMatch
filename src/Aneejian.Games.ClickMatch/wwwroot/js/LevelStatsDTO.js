
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
