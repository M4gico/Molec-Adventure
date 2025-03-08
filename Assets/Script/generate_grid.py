import matplotlib.pyplot as plt
import numpy as np

# Canvas size
width, height = 1920, 1080

# Subdivision levels
sub_x, sub_y = 4*3*3, 3*3*3  # 36 and 27 subdivisions

# Compute step sizes
step_x = width / sub_x
step_y = height / sub_y

# Create figure
fig, ax = plt.subplots(figsize=(width/100, height/100), dpi=100)

# Draw grid
for i in range(sub_x + 1):
    x = i * step_x
    color = 'gray' if i % 3 else 'lightgray' if i % 9 else 'white'
    ax.plot([x, x], [0, height], color=color, lw=2 if i % 9 else 4)

for j in range(sub_y + 1):
    y = j * step_y
    color = 'gray' if j % 3 else 'lightgray' if j % 9 else 'white'
    ax.plot([0, width], [y, y], color=color, lw=2 if j % 9 else 4)

# Hide axes
ax.set_xlim(0, width)
ax.set_ylim(0, height)
ax.set_xticks([])
ax.set_yticks([])
ax.set_frame_on(False)

# Show the grid
plt.gca().invert_yaxis()
plt.show()