'use strict';

const env = require('./../../utils/environment');

module.exports = {
    env: 'default',
    server: {
        port: env.read('IMPERIO_SERVER_LISTEN_PORT')
    },
    database: {
        host: env.read('IMPERIO_DATABASE_HOST'),
        database: env.read('IMPERIO_DATABASE_NAME'),
        username: env.read('IMPERIO_DATABASE_USER'),
        password: env.read('IMPERIO_DATABASE_PASSWORD'),
        logging: env.read('IMPERIO_DATABASE_CONSOLE_OUTPUT') === 'false' ? false : console.log,
        dialect: 'mysql'       
    }
};