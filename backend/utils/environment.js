'use strict'

const parse = Symbol('send');
const readPlain = Symbol('readPlain');

class Environment {
  read(variable) {
    try {
        const value = process.env[variable];
        return value != null && value != '' ? value : null;
      } catch (ex) {
        return null
      }
  }
}

module.exports = new Environment();