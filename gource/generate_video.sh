#!/bin/bash

gource  --seconds-per-day 0.3 --auto-skip-seconds 0.4 --max-file-lag 0.5 --bloom-multiplier 1.0 --bloom-intensity 1.0  --title "Survival of the Carrot People" --user-image-dir users --user-scale 3.0  -1280x720 -o - | ffmpeg -y -r 60 -f image2pipe -vcodec ppm -i - -vcodec libx264 -preset ultrafast -pix_fmt yuv420p -crf 1 -threads 0 -bf 0 gource2.mp4
