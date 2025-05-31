using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011_A2_Part2_ChloeF;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Ped _ped1; // Zero-argument constructor pedestrian
    private Ped _ped2; // Argumented constructor pedestrian
    private Texture2D _pedTexture; // Texture for both pedestrian types

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

    }

    protected override void Initialize()
    {
        // Creates ped1 with zero-argument constructor defaults
        _ped1 = new Ped();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _font = Content.Load<SpriteFont>("GameFont");

        // Loads texture for player pedestrian
        _pedTexture = Content.Load<Texture2D>("playerPed");

        _ped1 = new Ped(); // Create ped with default values and assign texture
        _ped1.SetTexture(_pedTexture);

        // Creates pedestrian with custom values, same texture
        _ped2 = new Ped(Color.Green, 4.5f, 1.0f, false, _pedTexture); 

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Update both ped objects for movement and rushing
        _ped1.Update();
        _ped2.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSlateGray);

        _spriteBatch.Begin();

        // Draws both pedestrians
        _ped1.Draw(_spriteBatch);
        _ped2.Draw(_spriteBatch);

        // Draws isRushing bool state
        _spriteBatch.DrawString(
            _font, $"Ped1 Rushing: {_ped1.GetIsRushing()}",
            new Vector2(10, 10),
            Color.White);

        _spriteBatch.DrawString(
            _font, $"Ped2 Rushing: {_ped1.GetIsRushing()}",
            new Vector2(10, 40),
            Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
