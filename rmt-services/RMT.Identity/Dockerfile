
# Build for development

FROM node:lts-alpine AS development

WORKDIR /usr/src/app

COPY --chown=node:node package*.json ./

RUN npm ci

COPY --chown=node:node . .

RUN npm run build

USER node

# Build for production

FROM node:lts-alpine AS build

WORKDIR /usr/src/app


COPY --chown=node:node package*.json ./

COPY --chown=node:node --from=development /usr/src/app/node_modules  ./node_modules

COPY --chown=node:node . .

RUN npm run build

ENV NODE_ENV=production

RUN npm ci --only=production && npm cache clean --force

RUN npm prune --production

USER node



# PRODUCTION

FROM node:lts-alpine AS production

COPY --chown=node:node --from=build /usr/src/app/. .

ENTRYPOINT ["sh" ,"-c", "./deployment.sh" ]
