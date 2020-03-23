FROM node:12.13

COPY package*.json ./

RUN rm -rf node_modules && npm install

COPY . .

COPY --chown=node:node . .

USER node

EXPOSE 80

CMD [ "node", "./index.js" ]