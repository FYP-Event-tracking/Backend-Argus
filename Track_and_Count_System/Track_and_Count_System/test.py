from flask import Flask, request,Response
from flask_cors import CORS
import datetime
import logging
import threading
import cv2
import logging
import time
import os

app = Flask(__name__)
CORS(app)

logging.basicConfig(level=logging.INFO)

frame = None
video_capture_active = False

def start_video_capture():
    global frame, video_capture_active
    video_capture_active = True
    cap = cv2.VideoCapture(0)
    cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640) 
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
    cap.set(cv2.CAP_PROP_FPS, 30) 
    
    while cap.isOpened() and video_capture_active:
        ret, frame = cap.read()
        if not ret:
            break
    
        logging.info(datetime.datetime.now())
        
        time.sleep(0.1)
    
    cap.release()
    video_capture_active = False


@app.route('/session', methods=['POST'])
def submit():
    data = request.json
    if 'Action' in data:
        if data['Action'] == 'Start':
            threading.Thread(target=start_video_capture, daemon=True).start()
            return "Video capture initiated"
        elif data['Action'] == 'Stop':
            global video_capture_active
            video_capture_active = False
            return "Video capture stopped"
    return "Invalid request"

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8009)
