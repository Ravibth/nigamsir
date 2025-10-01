/* eslint-disable @typescript-eslint/no-var-requires */
const https = require('https');
const fs = require('fs');
const streamDir = './.env';
const DOPPLER_TOKEN = 'dp.st.dev.hbJy8OdnpTchbm0lkHKEAD4htGbrLaEs6VP0BtPFdFq';
const TERMINAL_COLOR = '\x1b[0m';
const RED_FONT_COLOR = '\x1b[31m';
https
  .get(
    `https://${DOPPLER_TOKEN}@api.doppler.com/v3/configs/config/secrets/download?format=env`,
    (res) => {
      console.log(
        'downloading start, please look into env.json file at root directory',
      );

      const stream = fs.createWriteStream(streamDir);
      res.pipe(stream);
      res.on('end', () => {
        console.log('downloading complete');

        console.log(
          RED_FONT_COLOR,
          '** DO NOT ADD .env FILE INTO GIT CACHE. It must be in gitignore. ***',
          TERMINAL_COLOR,
        );
      });
    },
  )
  .on('error', (e) => {
    console.log('Error in downloading secrets. Please find details below');
    console.error('error', e);
  });
