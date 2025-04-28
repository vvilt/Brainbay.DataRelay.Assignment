FROM node:22.15.0-bullseye-slim

RUN npm install -g @angular/cli

# Enables recompile on change with dev containers
ENV CHOKIDAR_USEPOLLING=true

WORKDIR /workspace

EXPOSE 4501