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

        public Options(Dictionary<string, Texture2D> texturesDictionary, Matrix transformationMatrix)
        {
            slider = new Slider(100);
        }

        //public void drawOptions()
        //{
        //    slider.drawSlider();
        //}
        
    }
}
