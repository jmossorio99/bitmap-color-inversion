import time
import sys
from PIL import Image

# Access all eight images
image1 = Image.open('images/grabber1.bmp')
image2 = Image.open('images/grabber2.bmp')
image3 = Image.open('images/grabber3.bmp')
image4 = Image.open('images/grabber4.bmp')
image5 = Image.open('images/grabber5.bmp')
image6 = Image.open('images/grabber6.bmp')
image7 = Image.open('images/grabber7.bmp')
image8 = Image.open('images/grabber8.bmp')

# Save each image's dimensions and color matrix
width1, height1 = image1.size
width2, height2 = image2.size
width3, height3 = image3.size
width4, height4 = image4.size
width5, height5 = image5.size
width6, height6 = image6.size
width7, height7 = image7.size
width8, height8 = image8.size

widths = [width1, width2,  width3,  width4,  width5,  width6,  width7,  width8]
heights = [height1, height2, height3, height4, height5, height6, height7, height8]

colors1 = image1.load()
colors2 = image2.load()
colors3 = image3.load()
colors4 = image4.load()
colors5 = image5.load()
colors6 = image6.load()
colors7 = image7.load()
colors8 = image8.load()

colors = [colors1, colors2, colors3, colors4, colors5, colors6, colors7, colors8]

# This method will run one configuration of the experiment
def start_experiment(algorithm_version, image_version):
    if algorithm_version == 1:
        return algorithm_version_1(image_version - 1)
    elif algorithm_version == 2:
        return algorithm_version_2(image_version - 1)
    elif algorithm_version == 3:
        return algorithm_version_3(image_version - 1)
    elif algorithm_version == 4:
        return algorithm_version_4(image_version - 1)
    else:
        return algorithm_version_5(image_version - 1)

# Implement Algorithms to invert color matrix
# Version 1
def algorithm_version_1(index):
    start_time = time.time_ns()
    for i in range(widths[index]):
        for j in range(heights[index]):
            color = colors[index][i, j]
            colors[index][i, j] = (255 - color[0], 255 - color[1], 255 - color[2])
    end_time = time.time_ns()
    elapsed = end_time - start_time
    return elapsed

# Version 2
def algorithm_version_2(index):
    start_time = time.time_ns()
    for i in range(widths[index]):
        for j in range(heights[index]):
            color = colors[index][i, j]
            color = color[:0] + (255 - color[0],) + color[0+1:]
            colors[index][i, j] = color
    for i in range(widths[index]):
        for j in range(heights[index]):
            color = colors[index][i, j]
            color = color[:1] + (255 - color[1],) + color[1+1:]
            colors[index][i, j] = color
    for i in range(widths[index]):
        for j in range(heights[index]):
            color = colors[index][i, j]
            color = color[:2] + (255 - color[2],) + color[2+1:]
            colors[index][i, j] = color
    end_time = time.time_ns()
    elapsed = end_time - start_time
    return elapsed

# Version 3
def algorithm_version_3(index):
    start_time = time.time_ns()
    for i in range(heights[index]):
        for j in range(widths[index]):
            color = colors[index][i, j]
            colors[index][i, j] = (255 - color[0], 255 - color[1], 255 - color[2])
    end_time = time.time_ns()
    elapsed = end_time - start_time
    return elapsed

# Version 4
def algorithm_version_4(index):
    start_time = time.time_ns()
    for i in range(widths[index]):
        for j in range(heights[index]):
            color = colors[index][i, j]
            color = color[:0] + (255 - color[0],) + color[0+1:]
            colors[index][i, j] = color

    for i in range(widths[index] - 1, -1, -1):
        for j in range(heights[index] - 1, -1, -1):
            color = colors[index][i, j]
            color = color[:1] + (255 - color[1],) + (255 - color[2],) + color[2+1:]
            colors[index][i, j] = color
    end_time = time.time_ns()
    elapsed = end_time - start_time
    return elapsed

# Version 5
def algorithm_version_5(index):
    start_time = time.time_ns()
    for i in range(0, widths[index], 2):
        for j in range(0, heights[index], 2):
            color = colors[index][i, j]
            colors[index][i, j] = (255 - color[0], 255 - color[1], 255 - color[2])
            if i < widths[index] - 1 and j < heights[index] - 1:
                color = colors[index][i, j + 1]
                colors[index][i, j + 1] = (255 - color[0], 255 - color[1], 255 - color[2])

                color = colors[index][i + 1, j]
                colors[index][i + 1, j] = (255 - color[0], 255 - color[1], 255 - color[2])

                color = colors[index][i + 1, j + 1]
                colors[index][i + 1, j + 1] = (255 - color[0], 255 - color[1], 255 - color[2])
            elif i < widths[index] - 1:
                color = colors[index][i + 1, j]
                colors[index][i + 1, j] = (255 - color[0], 255 - color[1], 255 - color[2])
            elif j < heights[index] - 1:
                color = colors[index][i, j + 1]
                colors[index][i, j + 1] = (255 - color[0], 255 - color[1], 255 - color[2])
    end_time = time.time_ns()
    elapsed = end_time - start_time
    return elapsed

# algorithm_version_5(4)
# image5.show()

# nums = ['10609', '42849', '85264', '170569', '341056', '682276', '1364224', '2096704']
file = open("output.csv", "w")

for i in range(8):
    #file.write(f'==================Image Version: {i+1}=========================\n')
    for j in range(5):
        #file.write(f'=================Algorithm Version: {j+1}=======================\n')
        for i in range(60):
            file.write(f'{start_experiment(i+1, j+1)},')
        file.write('\n')

file.close()


