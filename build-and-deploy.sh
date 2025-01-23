#!/bin/bash

# Change to the project directory
cd ImageEventUI

# Install Angular CLI
npm install -g @angular/cli

# Install Angular CLI GitHub Pages
npm install -g angular-cli-ghpages

# Install Dependencies
npm install --prefix ImageEventUI

# Build the project
ng build --configuration production --base-href "https://the-running-dev.github.io/Demo-ImageEventProcessor/" --project ImageEventUI

# Configure Git
git config --global user.name 'github-actions[bot]'
git config --global user.email 'github-actions[bot]@users.noreply.github.com'

# Deploy to GitHub Pages
npx angular-cli-ghpages --dir=dist/image-event-ui/browser --no-silent