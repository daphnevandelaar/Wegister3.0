#################
# Build the app #
#################
ARG CONFIGURATION="production"

FROM node:12-alpine as build
ARG CONFIGURATION="production"
ENV CONFIGURATION=$CONFIGURATION
WORKDIR /app
COPY . ./

RUN npm install
RUN $(npm bin)/ng build --prod --configuration $CONFIGURATION --output-path=/dist

################
# Run in NGINX #
################
FROM nginx:alpine
COPY --from=build /dist /usr/share/nginx/html
COPY /etc/nginx/conf.d/default.conf /etc/nginx/conf.d/default.conf

# When the container starts, replace the env.js with values from environment variables
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]
