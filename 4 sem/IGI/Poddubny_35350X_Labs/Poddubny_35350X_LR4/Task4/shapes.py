from abc import ABC, abstractmethod
import re, math

class GeometicShape(ABC):

    @abstractmethod
    def get_square(self):
        pass

class Color:

    def __init__(self):
        self._color = None

    @property
    def color(self):
        return self._color

    @color.setter
    def color(self, value):
        if(is_valid_hex_color(value)):
            self._color = value
        else: raise Exception("not a hex")

    @color.deleter
    def color(self, value):
        del self._color
        
def is_valid_hex_color(color):
    pattern = r'^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$'
    return bool(re.fullmatch(pattern, color))

class Parallelogram(GeometicShape):

    _name = "parallelogram"

    @classmethod
    def get_name(cls):
        return cls._name

    def __init__(self, side1, side2, angle, color):
        if all(isinstance(x, int) for x in [side1, side2, angle]):            
            self.side1 = side1
            self.side2 = side2
            if angle <180 and angle > 0: self.angle = math.radians(angle)
            else: raise Exception("angle must be from 0 to 180")
        else: raise Exception("must be int")
        self.color_object = Color()
        self.color_object.color = color

    def get_square(self):
        angle = self.angle
        return self.side1 * self.side2 * math.sin(angle)
    
    def __str__(self):
        return "side 1: {}, side 2: {}, angle: {}, color: {}, square: {}".format(self.side1, self.side2, self.angle, self.color_object.color, self.get_square())