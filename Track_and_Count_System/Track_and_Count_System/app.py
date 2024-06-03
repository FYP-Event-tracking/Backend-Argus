from flask import Flask, request, jsonify
from flask_cors import CORS
from flask_socketio import SocketIO, emit
import datetime
import logging

app = Flask(__name__)
CORS(app) 
socketio = SocketIO(app)

logging.basicConfig(level=logging.INFO)

@socketio.on('connect')
def handle_connect():
    logging.info('Client connected')
    emit('status', {'status': 'Connected'})

@socketio.on('disconnect')
def handle_disconnect():
    logging.info('Client disconnected')
    emit('status', {'status': 'Disconnected'})

@socketio.on('videostream')
def handle_video_stream(data):
    logId = data.get('LogId')
    boxId = data.get('BoxId')
    itemType = data.get('ItemType')
    userId = data.get('UserId')
    startTime = data.get('StartTime')
    frame = data.get('frame')

    if frame is None:
        emit('error', {'error': 'No frame part'})
        return

    current_time = datetime.datetime.now().isoformat()
    logging.info(f"Frame received at {current_time}")

    logging.info(f"LogId: {logId}, BoxId: {boxId}, ItemType: {itemType}, UserId: {userId}, StartTime: {startTime}")

    emit('status', {'status': 'Frame received', 'timestamp': current_time})

if __name__ == '__main__':
    socketio.run(app, host='0.0.0.0', port=8006, allow_unsafe_werkzeug=True)

