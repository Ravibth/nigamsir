import { useEffect } from "react";

const useBlockRefreshAndBack = (isDirty) => {
  useEffect(() => {
    if (isDirty) {
      const handleBeforeUnload = (event) => {
        event.preventDefault();
        // Custom logic to handle the refresh
        // Display a confirmation message or perform necessary actions
      };
      window.addEventListener("beforeunload", handleBeforeUnload);
      return () => {
        window.removeEventListener("beforeunload", handleBeforeUnload);
      };
    }
  }, [isDirty]);
};

export default useBlockRefreshAndBack;
