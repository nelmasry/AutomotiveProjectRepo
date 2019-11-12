export class IVehicle{
    id: number;
    customerId: number;
    vehicleId: string;
    registrationNumber: string;
    lastPingDate: Date;
}
export class ICustomerModel{
    id:number;
    name: string;
    address: string;
    vehicles: IVehicle[]; 
}