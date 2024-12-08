export interface InputConfig<T = string> {
    label: string,
    type: 'Text' | 'Number',
    currentValue?: string,
    value?: T;
}