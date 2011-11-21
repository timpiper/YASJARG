using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using Tile_Engine;

namespace YASJARG
{
	public class Player :GameObject
	{
		private const int fixJump=35;
		private const int moveSpeed=7;
		private int jumpValue=fixJump;
		private int startJump;
		private int gravity = 2;
		public bool isJumping=false;
		private int speed = 10;
		public bool onPlatform = false;
		SoundEffect soundJump;

		
		

		public Rectangle BoundingPlayer
        {
            get
            {
                return new Rectangle(
                   	(int)this.worldLocation.X,
                    (int)this.worldLocation.Y,
                    72,
                    72);
            }
		}
		
		public Player (ContentManager content)
		{
			soundJump=content.Load<SoundEffect>(@"Music\jump");
			
			animations.Add("right", new AnimationStrip(content.Load<Texture2D>(@"Textures\player_right"),72,"right"));
			animations.Add("left", new AnimationStrip(content.Load<Texture2D>(@"Textures\player_left"),72,"left"));
			
			PlayAnimation("right");
			
			frameWidth = 72;
            frameHeight = 72;
			worldLocation.X=100;
			worldLocation.Y=515;
		}
		
		public void jump(){
			soundJump.Play();
			worldLocation.Y -= jumpValue;
			jumpValue -= gravity;
			if (onPlatform==false){
				if(worldLocation.Y == startJump){
					isJumping=false;
					jumpValue=fixJump;
				}
			}
			
			if(onPlatform==true){
				if (worldLocation.Y == 500){
					isJumping=false;
					jumpValue=fixJump;
				}
			}
		}
			
		
		
		public override void Update(GameTime gameTime){
			string newAnimation;
			newAnimation=processKeyboard();
			
			if (newAnimation != currentAnimation){
                    PlayAnimation(newAnimation);
                }
			//repositionCamera();
		}
		
		

		
		/*private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;

            if (screenLocX > 500)
            {
                Camera.Move(new Vector2(screenLocX - 500, 0));
            }

            if (screenLocX < 200)
            {
                Camera.Move(new Vector2(screenLocX - 200, 0));
            }
        }*/
		
		
		
		private string processKeyboard(){
            string newAnimation="";
			
			KeyboardState keyState = Keyboard.GetState();
			
			
			if(keyState.IsKeyDown(Keys.Right)){
				newAnimation = "right";
				worldLocation.X += speed;
			}
			
			if(keyState.IsKeyDown(Keys.Left)){
				newAnimation="left";
				worldLocation.X -= speed;
			}
			
			if(keyState.IsKeyDown(Keys.Space)&& isJumping==false){
				startJump = Convert.ToInt32(worldLocation.Y);
				isJumping=true;
			}
			
			if(isJumping==true){
				jump ();
			}
			
			if(keyState.IsKeyDown(Keys.None)){
				newAnimation=currentAnimation;
			}
			
			return newAnimation;
			
		}
	}
}

