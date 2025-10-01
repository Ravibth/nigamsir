import * as React from "react";
import Switch, { SwitchProps } from "@mui/material/Switch";
import { useEffect, useState } from "react";
import { styled } from "@mui/material";
const label = { inputProps: { "aria-label": "Size switch demo" } };

export const ToggleSwitch = (props: any) => {
  const IOSSwitch = styled((props: SwitchProps) => (
    <Switch
      focusVisibleClassName=".Mui-focusVisible"
      disableRipple
      {...props}
    />
  ))(({ theme }) => ({
    width: 42,
    height: 26,
    padding: 0,
    "& .MuiSwitch-switchBase": {
      padding: 0,
      margin: 2,
      transitionDuration: "300ms",
      "&.Mui-checked": {
        transform: "translateX(16px)",
        color: "#fff",
        "& + .MuiSwitch-track": {
          backgroundColor:
            theme.palette.mode === "dark" ? "#2ECA45" : "#00a7b5 ",
          opacity: 1,
          border: 0,
        },
        "&.Mui-disabled + .MuiSwitch-track": {
          opacity: 0.5,
        },
      },
      "&.Mui-focusVisible .MuiSwitch-thumb": {
        color: "#33cf4d",
        border: "6px solid #fff",
      },
      "&.Mui-disabled .MuiSwitch-thumb": {
        color:
          theme.palette.mode === "light"
            ? theme.palette.grey[100]
            : theme.palette.grey[600],
      },
      "&.Mui-disabled + .MuiSwitch-track": {
        opacity: theme.palette.mode === "light" ? 0.5 : 0.3,
      },
    },
    "& .MuiSwitch-thumb": {
      boxSizing: "border-box",
      width: 22,
      height: 22,
    },
    "& .MuiSwitch-track": {
      borderRadius: 26 / 2,
      backgroundColor: theme.palette.mode === "light" ? "#787878" : "#39393D",
      opacity: 1,
      transition: theme.transitions.create(["background-color"], {
        duration: 500,
      }),
    },
  }));

  const [check, setCheck] = useState(false);
  useEffect(() => {
    checkBoxValue();
  }, []);
  const checkBoxValue = () => {
    if (
      props.checked.attributeValue === "false" ||
      props.checked.attributeValue === "" ||
      props.checked.attributeValue === "0"
    ) {
      setCheck(false);
    } else {
      setCheck(true);
    }
  };
  return (
    <div>
      <IOSSwitch
        checked={check}
        {...label}
        size="small"
        disabled={props.disabled ?? false}
        onChange={(_, newValue) => {
          props.onChange(newValue);
          checkBoxValue();
        }}
      />
      {/* <Switch {...label} /> */}
    </div>
  );
};
