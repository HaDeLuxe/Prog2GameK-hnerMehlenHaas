using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie {
    class Camera {
        

        Vector2 cameraWorldPosition = new Vector2(0, 0);

        public void setCameraWorldPosition(Vector2 cameraWorldPosition) {
            this.cameraWorldPosition = cameraWorldPosition;
        }

        public Matrix cameraTransformationMatrix(Viewport viewport, Vector2 screenCentre) {
             Vector2 translation = -cameraWorldPosition + screenCentre;
             Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0);
             return cameraMatrix;
        }
    }
}
