import React, {
  useState,
  useRef,
  forwardRef,
  useContext,
  useImperativeHandle,
} from "react";
import {
  SnackbarContextProps,
  SnackbarContext,
} from "../../../../../contexts/snackbarContext";
import { ETextFieldRendererControlTypes } from "../../../../../common/interfaces/IConfigurationMaster";

const CustomCellEditor = forwardRef((props: any, ref) => {
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const [value, setValue] = useState(props.value);
  const [isValid, setIsValid] = useState(true);
  const refInput = useRef(null);

  useImperativeHandle(ref, () => ({
    getValue() {
      return value;
    },
    isCancelBeforeStart() {
      return false;
    },
    isCancelAfterEnd() {
      return !isValid;
    },
  }));

  const validateValue = (newValue) => {
    const rule = new RegExp(props?.schemaItem?.validationRegEx);

    if (!rule) return true;
    return rule.test(newValue) && Number.isInteger(Number(newValue));
  };

  const onChangeHandler = (event) => {
    const newValue = event.target.value;
    setValue(newValue);
    const isValueValid = validateValue(newValue);
    if (!isValueValid) {
      snackbarContext.displaySnackbar("Enter Valid value", "error");
    } else {
      snackbarContext.closeSnackbar();
    }
    setIsValid(isValueValid);
  };

  // Handle key events
  const onKeyDown = (event) => {
    if (event.key === "Enter") {
      if (isValid) {
        props.stopEditing();
      }
    } else if (event.key === "Escape") {
      props.stopEditing(true); // Cancel editing
    }
  };

  const onBlur = (event) => {
    const newValue = event.target.value;
    setValue(newValue);
    const isValueValid = validateValue(newValue);
    if (!isValueValid) {
      snackbarContext.displaySnackbar("Enter Valid value", "error");
    } else {
      snackbarContext.closeSnackbar();
      props.stopEditing();
    }
    setIsValid(isValueValid);
  };

  return (
    <div className="ag-cell-edit-wrapper">
      {!props?.schemaItem?.controlType ||
        (props?.schemaItem?.controlType &&
          props?.schemaItem?.controlType?.toLowerCase() ===
            ETextFieldRendererControlTypes.INTEGER.toLowerCase() && (
            <input
              ref={refInput}
              value={value}
              onChange={onChangeHandler}
              onBlur={onBlur}
              onKeyDown={onKeyDown}
              type={ETextFieldRendererControlTypes.INTEGER ? "number" : "text"}
              className={`ag-cell-edit-input ${!isValid ? "invalid" : ""}`}
              style={{
                width: "100%",
                height: "100%",
                border: isValid ? "1px solid #babfc7" : "1px solid red",
              }}
            />
          ))}
    </div>
  );
});

export default CustomCellEditor;
