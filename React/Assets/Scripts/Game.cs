public class Game
{
    private int score;
    private int lives;
    private int level;

    private Difficulty difficulty;
    private Round currentRound;

    public Game()
    {
        currentRound = new(3f, 5);
    }
}