FROM node:22.15.0-bullseye-slim AS builder

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

RUN npm run build -- --configuration production

FROM node:22.15.0-bullseye-slim

WORKDIR /app

COPY package*.json ./
RUN npm install --only=production

COPY --from=builder /app/dist/angular-material-rick-and-morty ./dist/angular-material-rick-and-morty

COPY server.js .

EXPOSE 8080

CMD ["node", "server.js"]
