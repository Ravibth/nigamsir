export interface IBuOfferingsMaster {
  bu_id: string;
  bu_mid: string;
  bu: string;
  bu_leader?: string;
  bu_leader_id?: string;
  offering_id: string;
  offering_mid: string;
  offering: string;
  offering_leader?: string;
  offering_leader_id?: string;
  solution_id: string;
  solution_mid: string;
  solution: string;
  solution_leader?: string;
  solution_leader_id?: string;
}

export type IBuOfferingsMasterList = IBuOfferingsMaster[];
