 using System;
 using System.Collections.Generic;
 using System.Linq;
 using Microsoft.Xna.Framework;
 using Microsoft.Xna.Framework.Audio;
 using Microsoft.Xna.Framework.Content;
 using Microsoft.Xna.Framework.GamerServices;
 using Microsoft.Xna.Framework.Graphics;
 using Microsoft.Xna.Framework.Input;
 using Microsoft.Xna.Framework.Media;
 using Tile_Engine;
 
 namespace YASJARG
 {
	
	 public struct PlayerData
 	 {
    	 public Vector2 Position;
     	 public bool IsAlive;
     	 public Color Color;
     	 public float Angle;
     	 public float Power;
 	 }
	
	
     public class Game1 : Microsoft.Xna.Framework.Game
     {
	 	SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        Texture2D backgroundTexture;
        int screenWidth;
        int screenHeight;
		Player player;
		GameObject block;
		Level m_Level;
		bool gameStarted =false ;
		Texture2D startScreen;
		Song mySong;

 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
			m_Level = new Level();
        }
 
		protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 640;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "YASJARG";
            base.Initialize();
        }
 
        protected override void LoadContent()
        {
			
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
 
			mySong=Content.Load<Song>(@"Music\bg_music");
			MediaPlayer.Play(mySong);
            backgroundTexture = Content.Load<Texture2D> (@"Textures\background");
			startScreen= Content.Load<Texture2D>(@"Textures\startscreen");
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
		    font= Content.Load<SpriteFont>(@"Fonts\SimpleFont");
			m_Level.Load(Content);
			
			player = new Player(Content);
			
			

        }
 
        protected override void UnloadContent()
        {
        }
 
        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
			player.Update(gameTime);
			//checkCollision();
            base.Update(gameTime);
        }
 
        protected override void Draw(GameTime gameTime){	
			spriteBatch.Begin();
			if(gameStarted == false){
				startscreen();
			}
			else
				startgame();
			
            spriteBatch.End();
 			base.Draw(gameTime);
        }
 
		
        private void DrawScenery(){
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }
		
		
		private void DrawText(){
        	spriteBatch.DrawString(font, "Testausgabe", new Vector2(20, 45), Color.White);
 		}
		
		private void startscreen(){
			KeyboardState state = Keyboard.GetState();
			GraphicsDevice.Clear(Color.CornflowerBlue);
			Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
			spriteBatch.Draw(startScreen,screenRectangle, Color.White);
			spriteBatch.DrawString(font,"Press Space to begin", new Vector2(545,300), Color.White);
			
			if(state.IsKeyDown(Keys.Space)){
				gameStarted=true;
			}
		}
		
		private void startgame(){
			DrawScenery();
			m_Level.Render(spriteBatch, player);
			player.Draw(spriteBatch);
		}
		
		
		
		
		
		
		
		public void checkCollision(){
			
			Console.WriteLine("Platform: "+player.onPlatform.ToString());
			if(player.BoundingPlayer.Intersects(block.BoundingBox)){
				if(player.worldLocation.Y < block.worldLocation.Y-5){
					player.isJumping=false;
					player.onPlatform=true;
					Console.WriteLine("Top");
				}
				
				
				if(player.worldLocation.X < block.worldLocation.X){
					player.worldLocation.X = block.worldLocation.X-block.texture.Width+8;
					Console.WriteLine("Left");
				}
				
				if(player.worldLocation.X > block.worldLocation.X){
					player.worldLocation.X = block.worldLocation.X+block.texture.Width;
					Console.WriteLine("Right");
				}
			
				

			}
			
		}
     }
 }