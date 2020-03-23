'use strict';

const Error = require('./error');

class Async {
  static handler(fn) {
    return function (req, res, next) {
      Promise.resolve(fn(req, res, next)).catch((err) => {
          if (err) {
            Error.handler(err, req, res, next);
          }
      });
    };
  }

  static async forEach(array, fn) {
    for (let index = 0; index < array.length; index++) {
      await fn(array[index], index, array);
    }
  }
}

module.exports = Async;