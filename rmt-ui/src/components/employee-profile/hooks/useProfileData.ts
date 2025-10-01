import { useEffect, useState } from "react";
import { EmployeeProfile } from "../interfaces/employeeProfile";
import { getEmployeeProfile } from "../services/employeeProfileService";
import { aboutmeConst } from "../constants";

export default function useProfileData(userEmail: string) {
    const [linkedInURL, setLinkedInURL] = useState<string>();
    const [mainProfileData, setMainProfileData] = useState<EmployeeProfile | null>(null);
    const [loading, setLoading] = useState(true);
    const [aboutMe, setAboutMe] = useState(aboutmeConst);
    const [error, setError] = useState<string | null>(null);
    
    useEffect(() => {
        const fetchProfileData = async () => {
            try {
                const response = await getEmployeeProfile(userEmail);
                const data: EmployeeProfile = response;
                setMainProfileData(data);
                setAboutMe(data.about_me || aboutmeConst);
                setLoading(false);
                setLinkedInURL(data.linkedin_url);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'An unknown error occurred');
                setLoading(false);
            }
        };

        fetchProfileData();
    }, [aboutMe]);

    return { mainProfileData, linkedInURL, loading, aboutMe, error, setAboutMe, setMainProfileData, setLinkedInURL };
}