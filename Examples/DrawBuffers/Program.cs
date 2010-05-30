﻿#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;


// http://bakura.developpez.com/tutoriels/jeux/utilisation-shaders-avec-opengl-3-x/
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vertex-array-objects-avec-opengl-3-x/

namespace ArcEngine.Examples
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class DrawBuffers : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (DrawBuffers game = new DrawBuffers())
					game.Run();
			}
			catch (Exception e)
			{
				// Oops, an error happened !
				MessageBox.Show(e.StackTrace, e.Message);
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public DrawBuffers()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Draw buffers example";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;


			#region Texture

			// Loads a texture and binds it to TUI 2
			Texture = new Texture("data/texture.png");
			Texture.HorizontalWrap = HorizontalWrapFilter.Repeat;
			Texture.VerticalWrap = VerticalWrapFilter.Repeat;
			Display.TextureUnit = 2;
			Display.Texture = Texture;
			Display.Shader.SetUniform("texture", 2);
			
			#endregion

			
			#region Buffer

			// Indices
			Indices = new int[]
			{
				0, 1, 2, 
				1, 2, 3
			};

			// Creates a position, color, texture buffer
			Buffer = BatchBuffer.CreatePositionColorTextureBuffer();


			// Vertex elements
			float[] vertices = new float[]
			{
				// Coord							Color									Texture
				100.0f,  100.0f,				1.0f, 0.0f, 0.0f, 1.0f,			0.0f,   0.0f,
				500.0f,  100.0f,				0.0f, 1.0f, 0.0f, 1.0f,			256.0f, 0.0f,
				100.0f,  500.0f,				0.0f, 0.0f, 1.0f, 1.0f,			0.0f,   256.0f,
				500.0f,  500.0f,				0.0f, 0.0f, 1.0f, 1.0f,			256.0f, 256.0f
			};
			Buffer.SetVertices(vertices);


			// Or set data one by one
			Buffer.AddPoint(new Point(100, 100), Color.FromArgb(255, 0, 0), new Point(0, 0));
			Buffer.AddPoint(new Point(500, 100), Color.FromArgb(0, 255, 0), new Point(256, 0));
			Buffer.AddPoint(new Point(100, 500), Color.FromArgb(0, 0, 255), new Point(0, 256));
			Buffer.AddPoint(new Point(500, 500), Color.FromArgb(0, 0, 255), new Point(256, 256));
			Buffer.Update();


			#region VAO
/*			
			Batch = new BatchBuffer();


			VertexBuffer.Bind(0, 2);
			Shader.BindAttrib(0, "in_position");

			ColorBuffer.Bind(1, 4);
			Shader.BindAttrib(1, "in_color");


			GL.BindVertexArray(0);
*/
			#endregion

			#endregion

		}



		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;
		}




		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the Escape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			// Draws the index buffer
			Display.DrawIndexBuffer(Buffer, BeginMode.Triangles, Indices);


			// Draws the batch buffer
			//Display.DrawUserBatch(Buffer, BeginMode.Triangles, 0, 3);
		}




		#region Properties

		/// <summary>
		/// Indices
		/// </summary>
		int[] Indices;


		/// <summary>
		/// Index buffer
		/// </summary>
		BatchBuffer Buffer;


		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;


		/// <summary>
		/// Texture
		/// </summary>
		Texture Texture;

		#endregion

	}
}
