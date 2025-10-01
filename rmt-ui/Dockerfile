# Build stage
FROM node:alpine AS builder

# Set working directory
WORKDIR /app

# Copy package files and install dependencies
COPY package.json package-lock.json ./
RUN npm ci --force

# Copy application source code and build
COPY . .
ARG ARG_Target=qa
RUN npm run build:${ARG_Target}

# Production stage
FROM nginx:alpine AS production

# Copy built files from the builder stage
COPY --from=builder /app/build /usr/share/nginx/html

# Copy Nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf
