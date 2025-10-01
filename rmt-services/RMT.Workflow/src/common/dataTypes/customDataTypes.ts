import { Sequelize, DataTypes, Utils } from "sequelize";
const ABSTRACT = DataTypes.ABSTRACT.prototype.constructor;
class SNOWFLAKE_VARIANT extends ABSTRACT {
  // Mandatory: complete definition of the new type in the database
  toSql() {
    return "VARIANT";
  }

  // Optional: validator function
  validate(value: any, options: any) {
    return typeof value === "object";
  }

  // Optional: sanitizer
  _sanitize(value: any) {
    // Force all numbers to be positive
    return value;
  }

  // Optional: value stringifier before sending to database
  _stringify(value: any) {
    return JSON.stringify(value);
  }

  // Optional: parser for values received from the database
  static parse(value: any) {
    console.log("Calling Cusotm D type");
    try {
      return JSON.parse(value);
    } catch (e) {
      return null;
    }
  }
}

// Mandatory: set the type key
SNOWFLAKE_VARIANT.prototype.key = SNOWFLAKE_VARIANT.key = "SNOWFLAKE_VARIANT";

// Mandatory: add the new type to DataTypes. Optionally wrap it on `Utils.classToInvokable` to
// be able to use this datatype directly without having to call `new` on it.
DataTypes["SNOWFLAKE_VARIANT"] = SNOWFLAKE_VARIANT;
Sequelize["SNOWFLAKE_VARIANT"] = Utils.classToInvokable(SNOWFLAKE_VARIANT);

export { DataTypes }; // Optional: disable escaping after stringifier. Do this at your own risk, since this opens opportunity for SQL injections.
