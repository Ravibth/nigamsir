import React, { useEffect, useState } from "react";
import Autocomplete from "@mui/material/Autocomplete";
import Chip from "@mui/material/Chip";
import TextField from "@mui/material/TextField";
import "./style.css";
import uniqBy from "lodash/uniqBy";
interface Option {
  label: string;
}

//Need to replace with skills api result
const masterSkills: Option[] = [
  {
    label: "Dotnet",
  },
  {
    label: "Azure",
  },
  {
    label: "Aws",
  },
  {
    label: "Google cloud",
  },
  {
    label: "Python",
  },
  {
    label: "C#",
  },
  {
    label: "Entity Framework",
  },
  {
    label: "SQL",
  },
  {
    label: "Html",
  },
  {
    label: "CSS",
  },
];
let options: Option[] = [
  // Add more options as needed
];

const Showskills = (props: any) => {
  const [selectedItems, setSelectedItems] = useState<Option[]>([]);
  const [fixedOptions, setFixedOptions] = useState<Option[]>(selectedItems);
  const [value, setValue] = useState([...fixedOptions]);

  useEffect(() => {
    if (props.skills?.length) {
      options = [];
      const skills: Array<string> = props.skills;
      skills.forEach((option) => {
        options.push({ label: option });
      });

      setFixedOptions(options);
      setValue(options);
      options = [...options, ...masterSkills];
      options = uniqBy(options, "label");
      setSelectedItems(options);
    } else {
      setSelectedItems(masterSkills);
    }
  }, [props]);

  useEffect(() => {
    props?.getSkills(value);
  }, [value]);

  return (
    <Autocomplete
      multiple
      id="fixed-tags-demo"
      value={value}
      onChange={(event, newValue) => {
        setValue([
          ...fixedOptions,
          ...newValue.filter((option) => fixedOptions.indexOf(option) === -1),
        ]);
      }}
      options={selectedItems}
      getOptionLabel={(option) => option.label}
      renderTags={(tagValue, getTagProps) =>
        tagValue.map((option, index) => (
          <Chip
            label={option.label}
            {...getTagProps({ index })}
            disabled={fixedOptions.indexOf(option) !== -1}
          />
        ))
      }
      style={{ width: 500 }}
      renderInput={(params) => (
        <TextField {...params} label="Skills" placeholder="Skills" />
      )}
    />
  );
};

export default Showskills;
