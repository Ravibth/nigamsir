import { useState } from 'react';

export const useProfileEdit = (initialData) => {
  
  const [editModes, setEditModes] = useState({
    aboutMe: false,
    industry: false,
    project: false,
    linkedIn: false,
    yearsOfExp: false,
    present_work_location:false,      
    experience_outside_gt:[],
  });

  const [tempData, setTempData] = useState(initialData);

  const handleEdit = (section) => {
    setEditModes(prev => ({ ...prev, [section]: true }));
  };

  const handleCancel = (section, resetData) => {
    setEditModes(prev => ({ ...prev, [section]: false }));
    setTempData(resetData);
  };

  const handleFieldChange = (section, value) => {
    setTempData(prev => ({ ...prev, [section]: value }));
  };

  const handleNestedFieldChange = (section, index, field, value) => {
    const updatedSection = [...tempData[section]];
    updatedSection[index] = { ...updatedSection[index], [field]: value };
    setTempData(prev => ({ ...prev, [section]: updatedSection }));
  };

  return {
    editModes,
    tempData,
    setTempData,
    handleEdit,
    handleCancel,
    handleFieldChange,
    handleNestedFieldChange,
    setEditModes
  };
};