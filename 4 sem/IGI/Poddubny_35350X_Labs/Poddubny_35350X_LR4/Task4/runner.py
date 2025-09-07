from Task4 import shapes
import numpy as np
import matplotlib.pyplot as plt
from matplotlib.patches import Polygon

class Task4:
    
    @classmethod
    def run(cls):
        try:
            side1 = int(input("enter first side: "))
            side2 = int(input("enter second side: "))
            angle = int(input("enter angle: "))
            color = input("enter color: ")
            name = input("enter name: ")
            parallelogram = shapes.Parallelogram(side1, side2, angle, color)
        

            shapes.Parallelogram._name = name

            draw(parallelogram)
        except Exception as e:
            print(e)

def draw(parallelogram: shapes.Parallelogram):
    a = (0, 0)
    b = (parallelogram.side1, 0)
    c = (parallelogram.side2 * np.cos(parallelogram.angle), 
         parallelogram.side2 * np.sin(parallelogram.angle))
    d = (b[0] + c[0], b[1] + c[1])

    vertices = [a, b, d, c, a]
    x, y = zip(*vertices)

    plt.figure(figsize=(8, 8))
    
    fill = Polygon(
        [a, b, d, c],
        closed=True,
        facecolor=parallelogram.color_object.color,
        edgecolor="none",
    )
    plt.gca().add_patch(fill)
    plt.axis('equal') 

    plt.axis('off')

    padding = 0.5
    plt.xlim(min(x) - padding, max(x) + padding)
    plt.ylim(min(y) - padding, max(y) + padding)

    plt.title(parallelogram.get_name())
    plt.savefig("Task4/colorful_parallelogram.png", bbox_inches='tight', pad_inches=0)
    
    print(str(parallelogram))