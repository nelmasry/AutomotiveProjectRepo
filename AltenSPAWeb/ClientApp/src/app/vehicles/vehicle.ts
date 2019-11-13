export class ICustomerModel {
    id: number;
    name: string;
    address: string;
    vehicles: IVehicle[];
}

export class IVehicle {
    id: number;
    customerId: number;
    vehicleId: string;
    registrationNumber: string;
    lastPingDate: Date;
    isonline: boolean;
    status: string;
}
