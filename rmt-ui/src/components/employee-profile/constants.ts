import { SkillLevel } from "./interfaces/employeeProfile";

export const aboutmeConst = `In this section, please write about yourself in a way that helps teams get to know you at a glance—and enables the marketing team to use this as a brief introduction for proposals. 
            You may want to include:
            - A summary of your professional background and domain expertise
            - The industries or clients you've worked with
            - Your areas of specialization (technical, functional, or leadership-oriented)
            - Any notable certifications or qualifications that add credibility to your profile

            Example:
            "I am an Associate Director in the Tax Consulting practice with over 12 years of experience advising Indian and global clients on a wide spectrum of direct tax matters. A qualified Chartered Accountant, I also hold certifications in International Taxation and BEPS Action Planning. 
            My sectoral exposure spans Technology, Financial Services, Consumer & Industrial Products, and Infrastructure, enabling a nuanced understanding of sector-specific tax challenges and opportunities. 
            I specialize in corporate tax advisory, cross-border taxation, transaction structuring, and litigation support, with a strong focus on aligning tax strategy with business objectives. 
            My experience includes managing large-scale engagements involving tax due diligence, group restructuring, and regulatory representation across jurisdictions."`;



export const skillColorMap: Record<SkillLevel, string> = {
  Excelled: "#9169CA",
  Skilled: "#B69BDC",
  Building : "#66CAD3",
  Starting: "#99DCE1",
};


export const EmpProfileEditButtonSx = {
  color: '#4B0082',
  borderColor: '#4B0082',
  textTransform: 'none',
  borderRadius: '10px',
  fontWeight: 500,  
}

export const NameConstants = {
  linkedIn:"linkedIn",
  yearsOfExp : "yearsOfExp",
  WorkExpInfo: "Work history including past employments.",
  AboutMeMinTextLimit: "Minimum 100 characters required",
  TrainingInfo:"Training and education qualifications",  
  PresentWorkLocation:"This field is intended to capture your current work location (Country/City/State).",
  ExperienceWithGT:"This section is auto populated based on your named allocations in GT projects. It gives visibility into your internal engagements and the work you have contributed to within the firm.",
  ExperienceOutsideGT:"Use this section to enter relevant experience prior to joining the firm, or to highlight key projects delivered during your time here that showcase your expertise.",
}