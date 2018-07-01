'''
김서방 찾기 기능을 구현한 모듈
''''

import numpy as np
import math
from math import cos, sin

def kim(user_x, user_y, kim_x, kim_y, compass):
    kim = np.array([kim_x, kim_y, 1])
    kim = kim.reshape(3,1)
    radian = math.radians(compass)

    cosine = cos(radian)
    sine = sin(radian)

    matrix = np.array([cosine, -sine, -cosine*user_x+sine*user_y,
                       sine, cosine, -sine*user_x-cosine*user_y,
                       0, 0, 1])
    matrix = matrix.reshape(3,3)

    new_coordinate = np.dot(matrix, kim)
    if new_coordinate[1] < -5:
        return '0'
    elif new_coordinate[1] > 5:
        return '2'
    else:
        return '1'