'use strict';

const dotenv = require('dotenv');
const mergewith = require('lodash.mergewith');
const path = require('path');

let defaults = {};
let custom = {};

try {   
    if (!['production'].includes(process.env.NODE_ENV)) {
        dotenv.config({ path: path.resolve(__dirname, '../../', '.env') });
    }

    defaults = require('./default');
    custom = require(`./${process.env.NODE_ENV || 'development'}`);
} catch(err) {
    console.log(err)
};

const customizer = (obj, src) => {
    if (Array.isArray(obj)) {
        return src;
    }
}

module.exports = mergewith({}, defaults, custom, customizer);