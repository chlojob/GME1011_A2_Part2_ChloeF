using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011_A2_Part2_ChloeF;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Ped _ped;
    private Texture2D _pedTexture;

    private List<Vehicle> _vehicles;
    private Texture2D _carTexture;
    private Texture2D _truckTexture;
    private Random _rng;

    private Carrot _carrot;
    private Texture2D _carrotTexture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

        _rng = new Random();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _font = Content.Load<SpriteFont>("GameFont");

        _pedTexture = Content.Load<Texture2D>("playerPed");
        _carTexture = Content.Load<Texture2D>("carImg");
        _truckTexture = Content.Load<Texture2D>("truckImg");
        _carrotTexture = Content.Load<Texture2D>("carrotImg");

        _carrot = new Carrot(_carrotTexture, new Vector2(_rng.Next(100, 700), 50));

        _vehicles = new List<Vehicle>();

        float[] laneY = { 150, 230, 390, 470 };

        foreach (float y in laneY)
        {
            int dir = (y < 300) ? -1 : 1;

            float laneSpeed;

            Color tint = new Color(_rng.Next(256), _rng.Next(256), _rng.Next(256), 255);

            if (y == 230 || y == 390) // inside lanes, fast
            {
                laneSpeed = 4f + (float)_rng.NextDouble() * 2f;
            }
            else // outer lanes, slow
            {
                laneSpeed = 2f + (float)_rng.NextDouble() * 1.5f;
            }

            for (int i = 0; i < 3; i++)
            {
                float spacing = 250f;
                float startX = i * spacing;

                Color color = new Color(_rng.Next(256), _rng.Next(256), _rng.Next(256), 255);

                if (y == 230 || y == 390)
                {
                    // inside lanes, fast cars
                    _vehicles.Add(new Car(_carTexture, new Vector2(startX, y), laneSpeed, dir, color));
                }
                else
                {
                    // outer lanes, slow trucks
                    _vehicles.Add(new Truck(_truckTexture, new Vector2(startX, y), laneSpeed, dir, color));
                }
            }

        }

        Color pedColor = new Color(_rng.Next(256), _rng.Next(256), _rng.Next(256), 255);

        _ped = new Ped(pedColor, 4.5f, 1.0f, false, _pedTexture);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _ped.Update();

        foreach (Vehicle v in _vehicles)
        {
            v.Update();

            if (v.GetCollisionRect().Intersects(_ped.GetCollisionRect()))
            {
                _ped.SetPosition(new Vector2(400, 550));
            }
        }

        if (_ped.GetCollisionRect().Intersects(_carrot.GetCollisionRect()))
        {
            if (_carrot.IsTop())
            {
                _carrot.SetPosition(new Vector2(_rng.Next(100, 700), 550)); // Bottom
                _carrot.SetIsTop(false);
            }
            else
            {
                _carrot.SetPosition(new Vector2(_rng.Next(100, 700), 50));  // Top
                _carrot.SetIsTop(true);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSlateGray);

        _spriteBatch.Begin();

        _ped.Draw(_spriteBatch);

        foreach (Vehicle v in _vehicles)
        {
            v.Draw(_spriteBatch);
        }

        _carrot.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
