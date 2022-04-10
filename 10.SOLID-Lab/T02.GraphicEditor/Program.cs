using System;

namespace T02.GraphicEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphicEditor graphicEditor = new GraphicEditor();
            graphicEditor.DrawShape(new Circle());
            graphicEditor.DrawShape(new Rectangle());
            graphicEditor.DrawShape(new Square());
        }
    }
}
