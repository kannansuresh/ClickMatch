# Click & Match

A simple game of matching pairs of cards. The game is written in C# and uses Blazor WebAssembly for the UI.

## How to Play Click & Match

Click & Match is a simple and fun memory game. The objective of the game is to match pairs of cards.

### Instructions

1. When the game starts, you will see a grid of facedown cards.
2. Click on a card to flip it over.
3. Then click on a second card to flip it over.
   - If the two cards match, they will remain face up and disappear.
   - If they do not match, they will flip back over.
4. The game continues until you have matched all pairs of cards.

### Scoring

- You get a multiplier when you match cards without making a miss.
- You get bonus points if you match two cards and one of them was flipped only once.
- You get penalty points if you flip a card that was already revealed once. Penalty points are calculated based on the level of the game. Lower the level, higher the penalty. For example, penalty will be higher in level 1 compared to level 3.

### Tips

- Try to remember the position of cards. This is a game of memory.
- Be patient, some patterns may take a while to come back to you.

### Profile and Stats

The game stores all data in your browser. If you change the browser or clear the browser data, you will lose your progress. If nobody else is playing on your device, the default guest account is enough.

Enjoy the game!

## Technical Aspects

The game is written in C# and uses Blazor WebAssembly for the UI. The game is hosted on GitHub Pages.

### How does the user profile and game data persistence work?

I have made use of IndexedDB to store user profiles and game data. Thanks to [Dexie.js](https://dexie.org/) for making it easy to work with IndexedDB.

### Does the project follow all the best practices in Blazor?

I am new to Blazor and still learning. If you find any issues or have any suggestions, please fee free to open an issue or a pull request.

## Attribution

- For Images Used in the Game
  - [Animal Icons from Figma](https://www.figma.com/community/file/1253367719553048712/animal-icon) designed by [HRK](https://www.figma.com/@HRK04).
  - [Animal Avatars from Figma](https://www.figma.com/community/file/1302328392768925757/380-animal-avatars-2-style-full-free) designed by [Aleks](https://www.figma.com/@aleksmakowski)
  - Animal and Toy Icons made by [Freepik](https://www.freepik.com/) from [www.flaticon.com](https://www.flaticon.com/).
  - Bootstrap Icons from [icons.getbootstrap.com](https://icons.getbootstrap.com/).
  - Business people icons from [Reshot](https://www.reshot.com/).
