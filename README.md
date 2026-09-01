# Rock Paper Scissors — Refactoring Interview

Timebox: **45 minutes**

This exercise is available in **C#** (`csharp/`) and **TypeScript** (`typescript/`). Use the language you were asked to work in.

## Candidate brief

Refactor `src/Game.cs` (C#) or `src/Game.ts` (TypeScript). It works, but it is a mess.

- **Do not edit the test file** (`tests/GameTests.cs` / `tests/Game.test.ts`).
- All tests must stay green.
- Two players per round.
- Moves can be text (`rock`, `paper`, `scissors`) or emojis, including mixed together.
- Keep calling `Play` for as many rounds as you like. `Play` does not return a winner.
- Call `Result()` to see who is winning: the player with more round wins, or `"Draw"` if tied.
- You may restructure the production file however you like, as long as `Game.Play` and `Game.Result` keep the same behaviour.

### C#

```bash
cd csharp
dotnet test
```

### TypeScript

```bash
cd typescript
npm install
npm test
```

## What good looks like

- Names that describe moves, outcomes, and rules
- One way to normalise a move (text + emoji)
- One way to decide a winner
- Dead code gone
- Tests still passing without being touched
