export interface WalletBasicInfo {
    id: string,
    name: string,
    cryptoCount: number,
}

export interface WalletDto {
    name: string,
    currencies: CryptocurrencyDto[]
}

export interface CryptocurrencyDto {
    name: string,
    value: number
}

export interface NewCryptoDto {
    walletId: string,
    name: string,
    value: number
}