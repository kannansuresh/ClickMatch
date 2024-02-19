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

Enjoy the game!

## Game Modes

Currently the game has only one mode (classic). Hopefully, in the future, I might add more game modes:

- [x] **Classic Mode**: Match all pairs of cards without any time limit.
- [ ] **Time Trial**: Match all pairs of cards within a time limit.
- [ ] **Walk Mode**: User can reveal all cards together for a few seconds and then they will be flipped back. User has to remember the position of cards and match them.
