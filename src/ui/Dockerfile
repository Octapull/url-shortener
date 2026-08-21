# Build stage
FROM node:22-alpine AS build
WORKDIR /app

# Copy package files
COPY package*.json ./
RUN npm install

# Copy source code and build
COPY . .
RUN npx ng build --configuration production

# Serve stage
FROM nginx:alpine AS final
# Remove default nginx static assets
RUN rm -rf /usr/share/nginx/html/*
# Copy build output to nginx html dir.
# Angular 17+ defaults to browser subfolder in dist. We use glob here if needed or just browser path.
COPY --from=build /app/dist/url-shortener-ui/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
