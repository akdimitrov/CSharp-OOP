using System;
using T02.GraphicEditor.Contracts;

namespace T02.GraphicEditor
{
    public class GraphicEditor
    {
        public void DrawShape(IShape shape)
        {
            Console.WriteLine($"I'm {shape.GetType().Name}");
        }
    }
}
