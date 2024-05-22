FROM python:3.11-slim

RUN apt-get update && apt-get install -y \
    libgl1-mesa-glx \
    libglib2.0-0

WORKDIR /app

COPY Track_and_Count_System/requirements.txt .

RUN pip install -r requirements.txt

COPY Track_and_Count_System/ .

EXPOSE 8006

CMD ["python", "app.py"]