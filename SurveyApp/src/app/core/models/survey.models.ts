export interface Survey {
    error: string;
    value: Survey;
    isSuccess: any;
    id: string;
    title: string;
    creatorId: string;
    options: SurveyOption[];
    totalVotes: number;
    createdAt: Date;
  }
  
  export interface SurveyOption {
[x: string]: any;
    id: string;
    text: string;
    percentage:number;
    voteCount: number;
  }
  
  export interface CreateSurveyRequest {
    title: string;
    options: string[];
  }
  
  export interface VoteRequest {
    surveyId: string;
    optionId: string;
  }