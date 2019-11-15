# Step 1: Build the app in image 'builder'
FROM node:12.8-alpine AS builder

WORKDIR /usr/src/app
COPY ./ClientApp/ .
#RUN yarn && yarn build
RUN npm install
RUN npm install -g @angular/cli@8.3.18
RUN ng build
#RUN ng serve --port 4588 --proxy-config proxy.config.json
# Step 2: Use build output from 'builder'

FROM nginx:stable-alpine
label version="1.0"

COPY nginx.conf /etc/nginx/nginx.conf

WORKDIR /usr/share/nginx/html
COPY --from=builder /usr/src/app/dist/APM/ .