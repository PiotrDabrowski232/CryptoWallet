export interface WalletBasicInfo {
    id: string,
    name: string,
    cryptoCount: number,
}

export interface WalletDto {
    name: string,
    currencies: CryptocurrencyDto[],
    conversionSwitched: boolean
}

export interface CryptocurrencyDto {
    id: string,
    name: string,
    value: number,
    description: string,
    coinPrice?: number
}

export interface NewCryptoDto {
    walletId: string,
    name: string,
    value: number
}

export interface ValidationErrors {
    Name?: string[];
    Value?: string[];
}

export interface UpdateCryptoDto {
    walletId?: string,
    id?: string,
    name?: string,
    value?: number
}
