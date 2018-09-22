using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Menus {
    class Options {

        Slider slider = null;

        public Options()
        {
            slider = new Slider(100, new Vector2(500,500));
        }

        public void drawOptions(SpriteBatch spriteBatch, Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix)
        {
            slider.drawSlider(spriteBatch, texturesDictionary, transformationMatrix);
        }

        public void Update()
        {
            slider.moveSlider();
        }

    }
}
