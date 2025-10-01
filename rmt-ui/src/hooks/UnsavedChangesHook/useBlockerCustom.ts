import React from "react";
import { BlockerFunction, useBlocker } from "react-router-dom";

const useBlockerCustom = (isDirty) => {
  let shouldBlock = React.useCallback<BlockerFunction>(
    ({ currentLocation, nextLocation }) =>
      isDirty && currentLocation.pathname !== nextLocation.pathname,
    [isDirty]
  );
  let blocker = useBlocker(shouldBlock);

  // Reset the blocker if the user cleans the form
  React.useEffect(() => {
    if (blocker.state === "blocked" && isDirty === false) {
      blocker.reset();
    }
  }, [blocker, isDirty]);

  const handleCancel = () => {
    blocker.reset();
    return false; // Block navigation
  };

  const handleConfirm = () => {
    // setIsDirty(false);
    blocker.proceed();
    return true; // Allow navigation
  };

  return { blocker, handleCancel, handleConfirm };
};

export default useBlockerCustom;
