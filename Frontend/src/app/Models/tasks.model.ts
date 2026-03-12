export class TasksModel {
    taskId: string;
    taskName: string;
    description: string;
    priority: Int16Array;
    dueDate: Date;
    projectId: string;
    assignedTo : string;
    status:number;
    isRunningTask?: boolean;
    isUpdateStatus : boolean;
    StartTime : Date;
    userId : string;
}